import { Component } from 'solid-js';
import Header from '../components/Header/header';
import HeaderLoggedIn from '../components/Header/headerLoggedIn';
import CreatePoll from '../components/Poll/Poll-Creation/createPoll';

const CreatePollPage: Component = () => {
    return (
        <div>
            <HeaderLoggedIn />
            <CreatePoll />
        </div>
    )
}

export default CreatePollPage;