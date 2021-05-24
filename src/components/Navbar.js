import React from 'react';
import './Navbar.css';

const Navbar = () => {
    return (
        <div>
            <div id='menu-outer' className=''>
                <div className='table'>
                    <ul id='horizontal-list' className=''>
                        <li><a href="#">Home</a></li>
                        <li><a href="#aboutme">About Me</a></li>
                        <li><a href="#experience">Experience</a></li>
                        <li><a href="#contact">Contact</a></li>
                    </ul>
                </div>
            </div>
        </div>
    )
}

export default Navbar;