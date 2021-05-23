import React from 'react';
import './Navbar.css';

const Navbar = () => {
    return (
        <div>
            <div id='menu-outer' className=''>
                <div className='table'>
                    <ul id='horizontal-list' className=''>
                        <li><a href="#aboutme">About Me</a></li>
                        <li><a href="#experience">Experience</a></li>
                        {/* <li><a href="#">Projects</a></li> */}
                        <li><a href="#">Contact</a></li>
                    </ul>
                </div>
            </div>
        </div>
    )
}

export default Navbar;