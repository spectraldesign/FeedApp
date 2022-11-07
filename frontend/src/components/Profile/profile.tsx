import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./profile.css";
import { useNavigate, NavLink } from '@solidjs/router';

interface Tester {
    fullNameTest: string;
    userNameTest: string;
    emailTest: string;
}

function Profile() {

    const [fullName, setFullName] = createSignal('');
    setFullName('Full name');
    const [userName, setUserName] = createSignal('');
    setUserName('Username');
    const [email, setEmail] = createSignal('');
    setEmail('Email');
    const [votes, setVotes] = createSignal(0);

    const authentic = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiam9sa29ob2wiLCJleHAiOjE2Njc4NjI1NTcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyNzEvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI3MS8ifQ.37CW0qtNXcss-3JbSTsGyhgQ5fI9RFLL0yxxNQ4yBfQ'
    // const tester = localStorage.getItem("token");

    const navigate = useNavigate();
    fetch('https://localhost:7280/api/user/self', {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
                
                // body: JSON.stringify(data)
            })
            .then(response => {
                if (response.status === 200) {
                    console.log(response);
                    // alert('Get request success');
                    return response.json();
                } else {
                    alert('Get request success invalid');
                }
            }
        )
            .then(data => {
                setFullName('Name: ' + data['firstname'] + ' ' + data['lastname']);
                setUserName('Username: ' + data['userName']);
                setEmail('Email: ' + data['email']);
            })
            .then(data => {
                fetch('https://localhost:7280/api/vote/myvotes', {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
                
                // body: JSON.stringify(data)
                })
                .then(response => {
                    if (response.status === 200) {
                        console.log(response);
                        // alert('Get request success');
                        return response.json();
                    } else {
                        alert('Get request success invalid');
                    }
                })
                .then(data => {
                    setVotes(data.length);
                })
            })

    return (
        <div>
            <div class="profile-page">
                <div class="circle">
                    <div class="picture">
                        <i class='fas fa-user-alt' style='font-size:80px;color:white;'></i>
                    </div>
                </div>
                {/* <input class="picture" value="&#9786;"></input> */}
                <div class="centerer">
                    <div class="firstname">
                        {/* {fullName} */}
                        {/* conditon: {cond() ? 'true' : 'false'} */}
                        {fullName}
                    </div>
                </div>
                <div class="centerer">
                    <div class="username">
                        {userName}
                        
                    </div>
                </div>
                <div class="centerer">
                    <div class="email">
                        {email}
                    </div>
                </div>
                <div class="bottom">
                    <div class="votes">{votes} votes</div>
                    <div class="seperator">|</div>
                    <div class="polls">3,120 polls</div>
                </div>
            </div>
        </div>
    );
  }

  export default Profile;

