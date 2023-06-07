import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import MessengerSignalRService, { MessagingApiModel, MessagingCardApiModel } from '../http/messengerSignalRService';

const MessengerPage: React.FC = () => {
    const messengerSignalRService = new MessengerSignalRService();

    const { companionId } = useParams<{ companionId: string }>(); // Получение параметра companionId из URL
    const [messagingCards, setMessagingCards] = useState<MessagingCardApiModel[]>([]);
    const [messaging, setMessaging] = useState<MessagingApiModel>({
      user1Id: 0,
      user2Id: Number(companionId), // Преобразование companionId в число и установка в user2Id
      messagesList: [],
    });
  
    useEffect(() => {
        messengerSignalRService.getMessagingCards().then((cards) => {
          setMessagingCards(cards);
        });
    
        messengerSignalRService.getMessaging(messaging.user1Id).then((messagingData) => {
          setMessaging(messagingData);
        });
      }, [messaging.user1Id]); // Добавление messaging.user1Id в список зависимостей, чтобы обновить эффект при изменении значения
      
  return (
    <div>
      <h1 className="text-center"> Листування з: <a href={`/userPage/${messaging.user2Id}`}>Companion</a></h1>
      <div className="container border border-primary rounded overflow-hidden">
        <div className="row align-items-start messanger">
          <div className="messaging-cards-list col-3">
            {messagingCards.map((card) => (
              <div key={card.userId} className={`container border border-primary rounded overflow-hidden messaging-card ${card.userId == messaging.user1Id ? 'selected' : ''}`}>
                <span className="position-relative">
                  <img className="avatar-img" src={`/api/user/getAvatarByName/${card.userName}`} alt={card.userName} />
                </span>
                &nbsp;
                <label>{card.userName}</label>
              </div>
            ))}
          </div>
          <div className="col-9">
            <div id="messaging-area" className="messages-box container border border-primary rounded">
              {messaging.messagesList.map((message) => (
                <div key={message.id} className={`message-block container border border-primary rounded overflow-hidden ${/*message.isMine*/true ? 'my-message' : 'companion-message'}`}>
                  <div className="row align-items-start">
                    <div className="col">
                      <a className="fst-italic" href={`/userPage/${message.authorId}`}>{message.authorId}</a>: {message.text}
                    </div>
                    <div className="col text-end">
                      <label className="fst-italic text-muted">{message.postedAt.toLocaleString()}</label>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default MessengerPage;
