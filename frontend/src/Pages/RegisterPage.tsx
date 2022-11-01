import { Component } from 'solid-js';
import HomeButton from '../components/Header/buttons/homeButton';
import Register from '../components/Register/Register';
import Header from '../components/Header/header';

const RegisterPage: Component = () => {
    return (
        <div>
            {/* <HomeButton /> */}
            <Header />
            <Register />
        </div>
    )
}

export default RegisterPage;