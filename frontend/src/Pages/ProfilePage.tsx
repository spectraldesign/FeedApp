import { Component } from 'solid-js';
import Profile from '../components/Profile/profile';
import Header from '../components/Header/header';

const ProfilePage: Component = () => {
    return (
        <>
            <Header />
            <Profile />
        </>
    )
}

export default ProfilePage;