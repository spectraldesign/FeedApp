import { Component } from 'solid-js';
import HomeButton from '../components/Header/buttons/homeButton';
import Login from '../components/Login/Login';

const LoginPage: Component = () => {
    return (
        <div>
            <HomeButton />
            <Login />
        </div>
    )
}

export default LoginPage;