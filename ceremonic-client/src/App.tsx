import React from 'react';
import Header from './components/Header';
import Footer from './components/Footer';

import AboutUs from './page/AboutUs';
import IndexPage from './page/IndexPage';


function App() {
  return (
    <div className='App'>
      <Header />
      {/* <AboutUs /> */}
      <IndexPage />
      <Footer />
    </div>  
    
  );
}

export default App;