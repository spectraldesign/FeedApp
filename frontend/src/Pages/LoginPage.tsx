import { Component } from 'solid-js';
import Header from '../components/Header/header';
import Login from '../components/Login/Login';

const LoginPage: Component = () => {
    return (
        <div>
            <Header />
            <Login />
        </div>
    )
}

export default LoginPage;