import { Component } from 'solid-js';
import Register from '../components/Register/Register';
import Header from '../components/Header/header';

const RegisterPage: Component = () => {
    return (
        <div>
            <Header />
            <Register />
        </div>
    )
}

export default RegisterPage;