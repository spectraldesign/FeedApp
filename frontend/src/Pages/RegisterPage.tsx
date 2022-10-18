import { Component } from 'solid-js';
import HomeButton from '../components/Header/buttons/homeButton';
import Register from '../components/Register/Register';

const RegisterPage: Component = () => {
    return (
        <div>
            <HomeButton />
            <Register />
        </div>
    )
}

export default RegisterPage;