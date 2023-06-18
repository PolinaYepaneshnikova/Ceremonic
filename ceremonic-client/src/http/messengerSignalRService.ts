import * as signalR from "@aspnet/signalr";
import { $authHost } from "./index";
import EventEmitter from "events";

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

export enum SignalServiceEvent {
  MessagingsIsUpdated = "messagingsIsUpdated",
  MessagesIsViewed = "messagesIsViewed",
}

export class MessengerSignalRService {
  private connection?: signalR.HubConnection;

  public events = new EventEmitter();

  public getMessaging = async (
    companionId: number
  ): Promise<MessagingApiModel> => {
    const response = await $authHost.get(
      `/api/Messenger/messaging/${companionId}`
    );

    const messaging: MessagingApiModel = response.data;

    const viewedMessageIDs: number[] = messaging.messagesList
      .filter((item) => item.notViewed && item.authorId === Number(companionId))

      .map((item) => item.id);

    // viewedMessageIDs.forEach(() => {
    //   messaging.messagesList.forEach((item )  => [
    //     if (ChatItem)
    //   ])
    // })

    if (viewedMessageIDs.length > 0) {
      this.messagesIsViewed(messaging.user2Id, viewedMessageIDs);
    }

    return messaging;
  };

  public getMessagingCards = async (): Promise<MessagingCardApiModel[]> => {
    const response = await $authHost.get("/api/Messenger/messagingCards");

    const cards = response.data;

    // let currentCard: MessagingCardApiModel | undefined = cards.find(
    //   (card) => card.userId == this.messaging.user2Id
    // );

    // if (this.messaging.user2Id && !currentCard) {
    //   cards.unshift({
    //     userId: this.messaging!.user2Id,
    //     userName: "compainer",
    //     countOfNotViewedMessages: 0,
    //   });
    // }

    return cards;
  };

  public sendMessage = (message: FormData): Promise<any> => {
    return $authHost
      .post("/api/Messenger/sendMessage", message)
      .catch((error) => {
        console.error("Error sending message:", error);
      });
  };

  public messagesIsViewed = (
    companionId: number,
    messageIds: number[]
  ): Promise<any> => {
    return $authHost
      .put(`/api/Messenger/messagesIsViewed/${companionId}`, messageIds)

      .catch((error) => {
        console.error("Error marking messages as viewed: ", error);
      });
  };

  public startConnection = async () => {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(import.meta.env.VITE_SERVER_ENDPOINT + "/hubs/MessengerHub", {
        skipNegotiation: true,

        transport: signalR.HttpTransportType.LongPolling,

        accessTokenFactory: () => localStorage.getItem("jwtString")!,
      })

      .build();

    try {
      await this.connection.start();

      this.subscribe();
    } catch (error) {
      console.log("Error while starting connection: " + error);
    }
  };

  public subscribe() {
    this.connection?.on(
      SignalServiceEvent.MessagingsIsUpdated,

      (cards: MessagingCardApiModel[]) => {
        return this.events.emit(SignalServiceEvent.MessagingsIsUpdated, {
          cards,
        });
      }
    );

    this.connection?.on(
      SignalServiceEvent.MessagesIsViewed,

      (companionId: number, viewedMessageIDs: number[]) => {
        return this.events.emit(SignalServiceEvent.MessagesIsViewed, {
          companionId,
          viewedMessageIDs,
        });
      }
    );
  }

  abortConnection = () => {
    this.connection?.stop().then(() => {
      console.log("Hub Connection Stoped!");
    });
  };

  // getNewMessages = (companionId: number): Promise<MessagingApiModel> => {
  //   return $authHost
  //     .get(`/api/Messenger/newMessages/${companionId}`)
  //     .then((response) => {
  //       const newMessages: MessageApiModel[] = response.data;

  //       let viewedMessageIDs: number[] = [];
  //       for (let mes of newMessages) {
  //         if (mes.id >= this.messaging.messagesList.length) {
  //           this.messaging.messagesList.push(mes);
  //         }
  //         if (mes.authorId == this.messaging.user2Id && mes.notViewed) {
  //           viewedMessageIDs.push(mes.id);
  //         }
  //       }

  //       if (viewedMessageIDs.length > 0) {
  //         this.messagesIsViewed(this.messaging.user2Id, viewedMessageIDs);
  //       }

  //       return this.messaging;
  //     })
  //     .catch((error) => {
  //       console.error('Error getting new messages:', error);

  //       return this.messaging;
  //     });
  // };
}

export default MessengerSignalRService;
