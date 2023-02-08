import React from 'react';
import './css/footer.css'
import insta from '../assets/image/footer/insta.png'
import facebook from '../assets/image/footer/facebook.png'

function Footer () {
  return (
    <div className="footer">
      <div className="left">
        <div><img src={insta} alt="Instagram" className='img_footer'/> ceremonic_ukraine</div>
        <div><img src={facebook} alt="Facebook" className='img_footer'/> ceremonic_ukraine</div>
      </div>
      <div className="right">
        <p>Україна, м. Харків, проспект Науки 14 <br />
        +(380) 66 96 19 232</p>
        <p>@ 2022 CeremonicTEAM. <br /> 
        Усі права захищені.</p>
      </div>
    </div>
  );
}

export default Footer;
