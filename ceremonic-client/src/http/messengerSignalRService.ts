import * as signalR from '@aspnet/signalr';
import { $authHost } from './index';

export type MessagingCardApiModel = {
    userId: number;
    userName: string;
    countOfNotViewedMessages: number;
};
  
export type MessageApiModel = {
    id: number;
    authorId: number;
    text: string;
    imageFileName: string;
    fileName: string;
    postedAt: Date;
    notViewed: boolean;
};
  
export type MessagingApiModel = {
    user1Id: number;
    user2Id: number;
    messagesList: MessageApiModel[];
};

export class MessengerSignalRService {
  private hubConnection?: signalR.HubConnection;
  messagingCards: MessagingCardApiModel[] = [];
  messaging: MessagingApiModel = {
    user1Id: 0,
    user2Id: 0,
    messagesList: [],
  };

  startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(import.meta.env.VITE_SERVER_ENDPOINT + '/hubs/MessengerHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => localStorage.getItem('jwtString')!,
      })
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Hub Connection Started!');
      })
      .then(() => {
        this.resetData();
      })
      .then(() => {
        this.messagingsIsUpdatedSetListener();
        this.messagesIsViewedSetListener();
      })
      .then(() => {
        this.getMessagingCards();
      })
      .catch(err => console.log('Error while starting connection: ' + err));
  };

  abortConnection = () => {
    this.hubConnection?.stop()
      .then(() => {
        console.log('Hub Connection Stoped!');
      });
  };

  resetData = () => {
    this.messaging = {
      user1Id: 0,
      user2Id: 0,
      messagesList: [],
    };
    this.messagingCards = [];
  };

  getMessagingCards = (): Promise<MessagingCardApiModel[]> => {
    return $authHost
      .get('/api/Messenger/messagingCards')
      .then((response) => {
        this.messagingCards = response.data; 
        
        let currentCard: MessagingCardApiModel | undefined = this.messagingCards.find(
            card => card.userId == this.messaging.user2Id
        );

        if (this.messaging.user2Id && !currentCard) {
            this.messagingCards.unshift({
              userId: this.messaging!.user2Id,
              userName: "compainer",
              countOfNotViewedMessages: 0
            });
        }

        return this.messagingCards;
      })
      .catch((error) => {
        console.error('Error getting messaging cards:', error);

        return this.messagingCards;
      });
  };

  getMessaging = (companionId: number): Promise<MessagingApiModel> => {
    return $authHost
      .get(`/api/Messenger/messaging/${companionId}`)
      .then((response) => {
        this.messaging = response.data;
        
        let viewedMessageIDs: number[] = [];
        for (let mes of this.messaging.messagesList) {
          if (mes.id >= this.messaging.messagesList.length) {
            this.messaging.messagesList.push(mes);
          }
          if (mes.authorId == this.messaging.user2Id && mes.notViewed) {
            viewedMessageIDs.push(mes.id);
          }
        }

        if (viewedMessageIDs.length > 0) {
          this.messagesIsViewed(this.messaging.user2Id, viewedMessageIDs);
        }

        return this.messaging;
      })
      .catch((error) => {
        console.error('Error getting messaging:', error);

        return this.messaging;
      });
  };

  sendMessage = (message: FormData): Promise<any> => {
    return $authHost
      .post('/api/Messenger/sendMessage', message)
      .catch((error) => {
        console.error('Error sending message:', error);
      });
  };

  messagingsIsUpdatedSetListener = () => {
    if (this.hubConnection) {
      this.hubConnection.on('messagingsIsUpdated', (messagingCards: MessagingCardApiModel[]) => {
        this.messagingCards = messagingCards;
        
        if (this.messagingCards.find(
            card => card.countOfNotViewedMessages > 0 && card.userId == this.messaging.user2Id
        )) {
            this.getNewMessages(this.messaging.user2Id);
         }
      });
    }
  };

  getNewMessages = (companionId: number): Promise<MessagingApiModel> => {
    return $authHost
      .get(`/api/Messenger/newMessages/${companionId}`)
      .then((response) => {
        const newMessages: MessageApiModel[] = response.data;
        
        let viewedMessageIDs: number[] = [];
        for (let mes of newMessages) {
          if (mes.id >= this.messaging.messagesList.length) {
            this.messaging.messagesList.push(mes);
          }
          if (mes.authorId == this.messaging.user2Id && mes.notViewed) {
            viewedMessageIDs.push(mes.id);
          }
        }

        if (viewedMessageIDs.length > 0) {
          this.messagesIsViewed(this.messaging.user2Id, viewedMessageIDs);
        }

        return this.messaging;
      })
      .catch((error) => {
        console.error('Error getting new messages:', error);

        return this.messaging;
      });
  };

  messagesIsViewed = (companionId: number, messageIds: number[]): Promise<any> => {
    return $authHost
      .post(`/api/Messenger/messagesIsViewed/${companionId}`, messageIds)
      .catch((error) => {
        console.error('Error marking messages as viewed:', error);
      });
  };

  messagesIsViewedSetListener = () => {
    if (this.hubConnection) {
      this.hubConnection.on('messagesIsViewed', (companionId: number, viewedMessageIDs: number[]) => {
        let card: MessagingCardApiModel | undefined
          = this.messagingCards.find(card => card.userId == companionId);

        if (card) {
          card.countOfNotViewedMessages = 0;
        }

        if (companionId == this.messaging.user2Id) {
          for (let id of viewedMessageIDs) {
            this.messaging.messagesList[id].notViewed = false;
          }
        }
      });
    }
  };
}

export default MessengerSignalRService;