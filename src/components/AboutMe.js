import React from 'react';
import './AboutMe.css';

const AboutMe = () => {
    return (
        <div className='aboutme-section'>
            <div className='background-image'>
                <div className='layer'></div>
            </div>
            <div className='aboutme-container'>
                <h1 className='aboutme-h1 right'>&lt;About Me&gt;</h1>
                <hr className='aboutme-hr'/>
                <p className='aboutme-p right'>
                    Based in Panama, bridge of the world and home of a wide variety of cultures.<br/><br/>
                    I've been writing code since 2015. <br/><br/>
                    Over the years, I've had the privilege of working on a few corporations, mainly focused on retail and healthcare (two very interesting fields), delivering high quality software that solves a large variety of business issues or needs.<br/><br/>

                    Coffee is always my main development tool.<br/>
                    Music lover.<br/>
                    Pets lover.<br/>
                </p>
                <hr className='aboutme-hr'/>
            </div>
        </div>
    );
};

export default AboutMe;