import { Component } from 'solid-js';
import MyPolls from '../components/Profile/Polls/myPolls';
import Header from '../components/Header/header';

const MyPollsPage: Component = () => {
    return (
        <div>
            <Header />
            <MyPolls />
        </div>
    )
}

export default MyPollsPage;