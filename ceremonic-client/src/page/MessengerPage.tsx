import React, { useEffect, useMemo, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import MessengerSignalRService, {
  MessageApiModel,
  MessagingApiModel,
  MessagingCardApiModel,
  SignalServiceEvent,
} from "../http/messengerSignalRService";
import { ChatList, MessageBox, Input, Button } from "react-chat-elements";
import { startTransition } from "react";
import "react-chat-elements/dist/main.css";
import "./css/messenger.css";
import { MESSENGER_ROUTE } from "../utils/constRoutes";

const useMessengerClient = ({ companionId }: { companionId: string }) => {
  const client = useMemo(() => {
    return new MessengerSignalRService();
  }, []);

  const [cards, setCards] = useState<MessagingCardApiModel[]>([]);

  const inputRef = useRef<HTMLTextAreaElement>(null);

  const [messaging, setMessaging] = useState<MessagingApiModel>({
    user1Id: 0,
    user2Id: Number(companionId), // Преобразование companionId в число и установка в user2Id
    messagesList: [],
  });

  const companion = useMemo(() => {
    return cards.find((one) => one.userId == Number(companionId));
  }, [cards, messaging]);

  const boot = async () => {
    if (companionId == "0") {
      const cards = await client.getMessagingCards();

      setCards(cards);

      return;
    }

    const [cards, messaging] = await Promise.all([
      client.getMessagingCards(),
      client.getMessaging(Number(companionId)),
      client.startConnection(),
    ]);

    startTransition(() => {
      setCards(cards);
      setMessaging(messaging);
    });
  };

  const stop = async () => {
    client.abortConnection();
  };

  const send = async () => {
    // input comp is really special butterfly
    const text = inputRef.current?.value ?? "";

    if (!text) return;

    inputRef.current!.value = "";

    const data = new FormData();

    data.append("Text", text);

    data.append("DestinationUserId", companionId);

    try {
      await client.sendMessage(data);
    } catch (error) {
      alert(`Something went wrong: ` + (error as Error).message);
    }
  };

  useEffect(() => {
    client.events.addListener(
      SignalServiceEvent.MessagingsIsUpdated,

      async ({ cards }: { cards: MessagingCardApiModel[] }) => {
        setCards(cards);

        if (!cards.some((one) => one.userId === Number(companionId))) return;

        setMessaging(await client.getMessaging(Number(companionId)));
      }
    );

    client.events.addListener(
      SignalServiceEvent.MessagesIsViewed,

      ({
        companionId,
        viewedMessageIDs,
      }: {
        companionId: number;
        viewedMessageIDs: number[];
      }) => {
        if (Number(messaging.user2Id) !== companionId) return;

        setMessaging({
          ...messaging,

          messagesList: messaging.messagesList.map((message) => {
            return {
              ...message,

              notViewed: message.notViewed
                ? !viewedMessageIDs.includes(message.id)
                : message.notViewed,
            };
          }),
        });
      }
    );
  }, []);

  return {
    client,
    inputRef,

    cards,
    companion,
    messaging,

    boot,
    stop,
    send,
  };
};

const Message = ({
  author,
  message,
  position,
}: {
  author: string;
  message: MessageApiModel;
  position: "left" | "right";
}) => {
  return (
    <MessageBox
      position={position}
      type="text"
      text={message.text}
      id={message.id}
      title={author}
      removeButton={false}
      status={message.notViewed ? "sent" : "read"}
      notch={false}
      retracted={false}
      date={new Date(message.postedAt)}
      focus={false}
      titleColor={position == "left" ? "#000" : "coral"}
      forwarded={false}
      replyButton={false}
    />
  );
};

const MessengerPage: React.FC = () => {
  const { companionId } = useParams<{ companionId: string }>(); // Получение параметра companionId из URL

  const noConversation = companionId === "0";

  const navigate = useNavigate();

  const { stop, boot, send, inputRef, messaging, cards, companion } =
    useMessengerClient({
      companionId: companionId as string,
    });

  const dataSource = useMemo(() => {
    return cards.map((item, index) => ({
      id: item.userId,
      avatar: "https://placehold.co/48",
      title: item.userName,
      subtitle: "Last message text",
      date: new Date(),
      unread: 0,
    }));
  }, [cards]);

  const onConversationClick = (id: number) => [
    navigate(MESSENGER_ROUTE.replace(":companionId", id.toString())),
  ];

  useEffect(() => {
    boot();

    return () => {
      stop();
    };
  }, [companionId]);

  return (
    <div className="messenger-page-container">
      <h1>Messenger</h1>

      <div className="messenger-chat">
        <ChatList
          className="messenger-chat-list"
          id="test"
          lazyLoadingImage="https://placehold.co/48"
          dataSource={dataSource}
          onClick={(payload) => {
            onConversationClick(Number(payload.id));
          }}
        />

        {!noConversation && (
          <div className="messenger-chat-conversation">
            <div className="messenger-chat-messages">
              {messaging.messagesList.map((message) => {
                const owner = messaging.user1Id === message.authorId;

                return (
                  <Message
                    author={owner ? "You" : companion?.userName!}
                    message={message}
                    key={message.id}
                    position={
                      messaging.user1Id === message.authorId ? "left" : "right"
                    }
                  />
                );
              })}
            </div>

            <div>
              <Input
                referance={(ref: any) => {
                  // @ts-expect-error types issues
                  inputRef.current = ref;
                }}
                maxHeight={100}
                placeholder="Type here..."
                multiline={true}
                rightButtons={
                  <Button
                    color="white"
                    backgroundColor="black"
                    text="Send"
                    onClick={() => send()}
                  />
                }
              />
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default MessengerPage;
