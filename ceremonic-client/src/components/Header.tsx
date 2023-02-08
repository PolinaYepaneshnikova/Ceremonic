import React from 'react';
import styles from "./css/header.module.css"
import nameProduct from "../assets/image/header/NameProduct.png"
import { ReactComponent as Vector } from '../assets/image/header/Vector.svg'
import Button from './Button';

function Header() {
  return (
    <div className={styles.header}>
      <img src={nameProduct} className={styles.nameProduct} alt='Ceremonic' width={112} height={23}></img>
      <div className={styles.center}>
        <Button kind='primary'>Планування</Button>
        <Button kind='primary'>Місця</Button>
        <Button kind='primary'>Послуги</Button>
        <Button kind='primary'>Статті</Button>
        <Button kind='primary'>Тест</Button>
        <Button kind='primary'>Про нас</Button>
      </div>
      <div className={styles.auth}>
        <div className={styles.supplier}><Vector className='vector' /> &nbsp; Ви постачальник?</div>
        <Button kind='secondary' >Увійти</Button>
        <Button kind='secondary' >Зареєструватися</Button>

      </div>
    </div>
  );
}

export default Header;
