import React from 'react';
import './css/indexPage.css';
import Header from '../components/Header';
import Footer from '../components/Footer';
import Button from '../components/Button';

const IndexPage = () => {

    return(
        <div className="index-page">
            <div className="search">
                <div className="search-text">
                    <p>
                        Шукаєш ідеальне місце проведення церемонії для свого весілля або послугу? 
                    </p>

                    <p>
                        Спеціалісти зі всієї України зібралися тут, щоб зробити ваше весілля незабутнім.
                    </p>

                    <div className="buttons-search">

                    </div>

                    <div className="button-services">
                        <p>
                        Не знаєте, що саме шукати?
                        </p>
                        <Button kind='secondary'>Подивитися усі послуги</Button>
                    </div>

                </div>
            </div>
        </div>
    );
}

export default IndexPage;