import { Component } from 'solid-js';
import Profile from '../components/Profile/profile';
import Header from '../components/Header/header';
import Logout from '../components/Logout/Logout';
import CreatePollButton from '../components/Profile/Header/createPollButton';

const ProfilePage: Component = () => {
    return (
        <>
            <Header />
            <CreatePollButton />
            <Profile />
            <Logout />
        </>
    )
}

export default ProfilePage;