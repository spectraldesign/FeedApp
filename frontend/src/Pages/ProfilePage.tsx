import { Component } from 'solid-js';
import Profile from '../components/Profile/profile';
import Header from '../components/Header/header';
import Logout from '../components/Logout/Logout';

const ProfilePage: Component = () => {
    return (
        <>
            <Header />
            <Profile />
            <Logout />
        </>
    )
}

export default ProfilePage;