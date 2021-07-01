import React from 'react';
import './Contact.css';
import icon_chat from '../media/images/chat.svg';

const DEFAULT_STATE = {
    message: "",
    email: "",
    name: "",
    subject: "Portfolio - New message!",
    nameErrorVisibility: "hidden",
    emailErrorVisibility: "hidden",
    messageErrorVisibility: "hidden",
    apiResponseMessage: "",
    apiResponseSucceeded: true
}

const API_KEY = process.env.REACT_APP_API_KEY;
const API_ENDPOINT_BASE = process.env.REACT_APP_API_ENDPOINT_BASE;
const API_ENDPOINT_MAIL = process.env.REACT_APP_API_ENDPOINT_MAIL;
const EMAIL_DESTINATION = process.env.REACT_APP_EMAIL_TO;

class Contact extends React.Component {
    constructor() {
        super();
        
        this.state = { ...DEFAULT_STATE };

        this.setMessage = this.setMessage.bind(this);
        this.setName = this.setName.bind(this);
        this.setEmail = this.setEmail.bind(this);
        this.setNameErrorVisibility = this.setNameErrorVisibility.bind(this);
        this.setEmailErrorVisibility = this.setEmailErrorVisibility.bind(this);
        this.setMessageErrorVisibility = this.setMessageErrorVisibility.bind(this);
        this.setApiResponseSucceeded = this.setApiResponseSucceeded.bind(this);
        this.setApiResponseMessage = this.setApiResponseMessage.bind(this);
        this.clearState = this.clearState.bind(this);
    }

    async SendMailAsync() {
        if (!this.state.name) {
            this.setNameErrorVisibility("unhidden");
            return;
        } else {
            this.setNameErrorVisibility("hidden");
        }

        if (!this.state.email) {
            this.setEmailErrorVisibility("unhidden");
            return;
        } else {
            this.setEmailErrorVisibility("hidden");
        }

        if (!this.state.message) {
            this.setMessageErrorVisibility("unhidden");
            return;
        } else {
            this.setMessageErrorVisibility("hidden");
        }

        fetch(`${API_ENDPOINT_BASE}${API_ENDPOINT_MAIL}`, {
            headers: {
                'Content-Type': 'application/json',
                'X-Api-Key': API_KEY
            },
            method: 'POST',
            body: JSON.stringify({
                message: this.state.message,
                to: EMAIL_DESTINATION,
                subject: this.state.subject,
                senderName: this.state.name,
                from: this.state.email
            })
        })
        .then(res => {
            this.setApiResponseSucceeded(true);
            this.setApiResponseMessage(">> Message sent successfully :)");
        }).catch(ex => {
            this.setApiResponseSucceeded(false);
            this.setApiResponseMessage(">> There was an error sending your message. Please try again later.");
            console.warn('Fetch exception');
            console.error(ex);
        });

        this.clearState();
    }

    clearState() {
        this.setState({...DEFAULT_STATE});
    }

    setMessage(event) {
        this.setState({ message: event.target.value });
    }

    setName(event) {
        this.setState({ name: event.target.value });
    }

    setEmail(event) {
        this.setState({ email: event.target.value });
    }

    setNameErrorVisibility(value) {
        this.setState({ nameErrorVisibility: value });
    }

    setEmailErrorVisibility(value) {
        this.setState({ emailErrorVisibility: value });
    }

    setMessageErrorVisibility(value) {
        this.setState({ messageErrorVisibility: value });
    }

    setApiResponseSucceeded(value) {
        this.setState({ apiResponseSucceeded: value });
    }

    setApiResponseMessage(value) {
        this.setState({ apiResponseMessage: value });
    }

    render() {
        return (
        <div className='contact'>
             <div className='contact-box'>
                 <div className='header'>
                     <h1>Let's talk</h1>
                     <img alt='chat' src={icon_chat}/>
                 </div>
                
                 <input type='text' placeholder='Your name' value={this.state.name} onChange={this.setName} />
                 <label className={`error name ${this.state.nameErrorVisibility}`} style={{display: ''}}>* Please enter your name</label>
                 
                 <input type='email' placeholder='Your email' value={this.state.email} onChange={this.setEmail}/>
                 <label className={`error email ${this.state.emailErrorVisibility}`} style={{display: ''}}>* Please enter your email</label>

                 <textarea placeholder='Your message' value={this.state.message} onChange={this.setMessage}></textarea>
                 <label className={`error message ${this.state.messageErrorVisibility}`} style={{display: ''}}>* Please enter your message</label>
                 
                 <input type='button' value='Send message' onClick={() => this.SendMailAsync()}/>
                 <label className={`apiresponse ${this.state.apiResponseSucceeded ? "success" : "error"}`}>{this.state.apiResponseMessage}</label>
             </div>
         </div>
         );
    }
}

export default Contact;