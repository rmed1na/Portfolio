import React from 'react';
import './Contact.css';
import icon_chat from '../media/images/chat.svg';

const Contact = () => {
    return (
        <div className='contact'>
            <div className='contact-box'>
                <div className='header'>
                    <h1>Let's talk</h1>
                    <img alt='chat' src={icon_chat}/>
                </div>
                
                <input type='text' placeholder='Your name'/>
                <input type='email' placeholder='Your email'/>
                <textarea placeholder='Your message'></textarea>
                <input type='button' value='Send message'/>
            </div>
        </div>
    );
}

export default Contact;