import React, { useState, useEffect } from 'react';

interface TimerProps {
  duration: number;
}

const Timer: React.FC<TimerProps> = ({ duration }) => {
  const [timeRemaining, setTimeRemaining] = useState(duration);

  useEffect(() => {
    let intervalId: number;

    if (timeRemaining > 0) {
      intervalId = setInterval(() => {
        setTimeRemaining((prevTime) => prevTime - 1);
      }, 1000);
    }

    return () => {
      clearInterval(intervalId);
    };
  }, [timeRemaining]);

  const formatTime = (time: number): JSX.Element => {
    const days = Math.floor(time / 86400);
    const hours = Math.floor((time % 86400) / 3600);
    const minutes = Math.floor((time % 3600) / 60);

    `${days}день ${hours}годин ${minutes}секунд`
    return (
        <>
          <span style={{ color: '#00889A' }}>{days} </span>день
          <span style={{ color: '#00889A' }}> {hours} </span>годин <span style={{ color: '#00889A' }}>{minutes} </span>хвилин
        </>
      )
  }

  return (
    <>
      {formatTime(timeRemaining)}
    </>
  );
};

export default Timer;
