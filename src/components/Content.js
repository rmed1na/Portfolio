import './Content.css';
import React from 'react';
import Navbar from './Navbar';
import Home from './Home';
import AboutMe from './AboutMe';
import Experience from './Experience';

const Content = () => {
    return (
        <div>
            <Navbar/>

            <section id='home' className='section '>
                <Home/>
            </section>
            <section id='aboutme' className='section '>
                <AboutMe/>
            </section>
            <section id='experience' className='section'>
                <Experience/>
            </section>
        </div>        
    );
}

export default Content;