import { Component } from 'solid-js';
import Header from '../components/Header/header';
import CreatePoll from '../components/Poll/Poll-Creation/createPoll';
import SearchPoll from '../components/Poll/Poll-Search/searchPoll';
import APIButton from '../components/API/apiButton';

const Homepage: Component = () => {
    return (
        <div>
            <Header />
            <SearchPoll />
            <APIButton />
            {/* Uncomment line below to check create poll setup */}
            {/* <CreatePoll /> */}
        </div>
    )
}

export default Homepage;
