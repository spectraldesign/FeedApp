import { createSignal, createEffect, createMemo } from "solid-js";
import "./profile.css";
import { useNavigate, NavLink } from '@solidjs/router';
import CreatePollButton from "./Buttons/createPollButton";
import MyPollButton from "./Buttons/myPollButton";

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
    const [polls, setPolls] = createSignal(0);

    const token = localStorage.getItem("token");
    var authentic = token?.substring(1, token.length-1);

    const navigate = useNavigate();
    fetch(`${import.meta.env.VITE_BASE_URL}user/self`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
            })
            .then(response => {
                if (response.status === 200) {
                    console.log(response);
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
            fetch(`${import.meta.env.VITE_BASE_URL}vote/myvotes`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
                })
                .then(response => {
                    if (response.status === 200) {
                        console.log(response);
                        return response.json();
                    } else {
                        alert('Get request invalid');
                    }
                })
                .then(data => {
                    setVotes(data.length);
                })
            fetch(`${import.meta.env.VITE_BASE_URL}poll/myPolls`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
                })
                .then(response => {
                    if (response.status === 200) {
                        console.log(response);
                        return response.json();
                    } else {
                        alert('Get request invalid');
                    }
                })
                .then(data => {
                    setPolls(data.length);
                })
    return (
        <div>
            <div class="profile-page">
                <div class="circle">
                    <div class="picture">
                        <i class='fas fa-user-alt fa-spin' style='font-size:80px;color:white;'></i>
                    </div>
                </div>
                <div class="pollButtons">
                    <CreatePollButton />
                    <MyPollButton />
                </div>
                <div class="centerer">
                    <div class="firstname">
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
                    <div class="polls">{polls} polls</div>
                </div>
            </div>
        </div>
    );
  }

  export default Profile;

