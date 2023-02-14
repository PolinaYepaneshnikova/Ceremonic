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
        <Button kind='button_primary'>Планування</Button>
        <Button kind='button_primary'>Місця</Button>
        <Button kind='button_primary'>Послуги</Button>
        <Button kind='button_primary'>Статті</Button>
        <Button kind='button_primary'>Тест</Button>
        <Button kind='button_primary'>Про нас</Button>
      </div>
      <div className={styles.auth}>
        <div className={styles.supplier}><Vector className={styles.vector} /> &nbsp; Ви постачальник?</div>&nbsp;
        <Button kind='button_secondary' >Увійти</Button>
        <Button kind='button_secondary' >Зареєструватися</Button>

      </div>
    </div>
  );
}

export default Header;
