import React from 'react';
import './Home.css';

// Images & Icons
import profilePicture from '../media/images/rolandomedina-2.jpg';
import gitHubGoldIcon from '../media/images/github-gold.png';
import linkedInGoldIcon from '../media/images/linkedin-gold.png';
import nugetGoldIcon from '../media/images/nuget-gold.png';
import instagramGoldIcon from '../media/images/instagram-gold.png';
import icon_mail from '../media/images/mail.svg';

const Home = () => {
    return (
        <div className='container-home'>
            <div className='profile-picture'>
                <img src={profilePicture} alt='Profile picture'/>
            </div>
            
            <h1 className='home-h1'>Rolando Medina</h1>
            <h3 className='home-h3'>Software Engineer</h3>
            
            <div className='icongroup-social'>
                <a href='https://github.com/rmed1na' target='_blank'>
                    <img src={gitHubGoldIcon} alt='GitHub'/>
                </a>
                <a href='https://www.linkedin.com/in/rmed1na/' target='_blank'>
                    <img src={linkedInGoldIcon} alt='LinkedIn'/>
                </a>
                <a href='https://www.instagram.com/rmed1na/' target='_blank'>
                    <img src={instagramGoldIcon} alt='Instagram'/>
                </a>
                <a href='mailto:rolando.ms@outlook.com'>
                    <img src={icon_mail} alt='email'/>
                </a>
            </div>

            <p className='home-p'>Just a tech enthusiast who finds joy in solving real life problems <br/>with a few bytes of code</p>
        </div>
    );
}

export default Home;