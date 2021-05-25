import React from 'react';
import './Experience.css';
import icon_netFramework from '../media/images/NET_Logo.svg';
import icon_netCore from '../media/images/NET_Core_Logo.svg';
import icon_csharp from '../media/images/csharp.svg';
import icon_sqlServer from '../media/images/microsoft-sql-server.png';
import icon_aspnetcore from '../media/images/aspnetcore.svg';
import icon_efcore from '../media/images/ef-core-logo.png';
import icon_docker from '../media/images/docker.svg';
import icon_javascript from '../media/images/javascript.svg';
import icon_html5 from '../media/images/HTML5_Badge.svg';
import icon_css3 from '../media/images/css3.svg';
import icon_react from '../media/images/React-icon.svg';
import icon_redis from '../media/images/redis.png';
import icon_git from '../media/images/Git_icon.svg';
import icon_gitHub from '../media/images/github-gold.png';
import icon_visualStudio from '../media/images/visualstudio.png';
import icon_vscode from '../media/images/vscode.png';
import icon_mysql from '../media/images/mysql.png';

const Experience = () => {
    return (
        <div className='experience'>
            <div id='background'>
                <div className='layer'></div>
            </div>
            <div className='content '>
                <h1>4+ years writing code</h1>
                <div className='tech-items'>
                    <div className='tech-item'>
                        <img alt='.NET Framework' src={icon_netFramework}/>
                        <h3>.NET Framework</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='.NET Core' src={icon_netCore}/>
                        <h3>.NET Core</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='Csharp' src={icon_csharp}/>
                        <h3>C sharp</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='ASP.NET' src={icon_aspnetcore}/>
                        <h3>ASP.NET Core</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='SQL Server' src={icon_sqlServer}/>
                        <h3>SQL Server</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='EF Core' src={icon_efcore}/>
                        <h3>EF Core</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='Docker' src={icon_docker}/>
                        <h3>Docker</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='JavaScript' src={icon_javascript}/>
                        <h3>JavaScript</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='HTML5' src={icon_html5}/>
                        <h3>HTML5</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='CSS3' src={icon_css3}/>
                        <h3>CSS3</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='MySQL' src={icon_mysql}/>
                        <h3>MySQL</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='React' src={icon_react}/>
                        <h3>React</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='Redis' src={icon_redis}/>
                        <h3>Redis</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='Git' src={icon_git}/>
                        <h3>Git</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='GitHub' src={icon_gitHub}/>
                        <h3>GitHub</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='Visual Studio' src={icon_visualStudio}/>
                        <h3>Visual Studio</h3>
                    </div>
                    <div className='tech-item'>
                        <img alt='VS Code' src={icon_vscode}/>
                        <h3>VS Code</h3>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Experience;