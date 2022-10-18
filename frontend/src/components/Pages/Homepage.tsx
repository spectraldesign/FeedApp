import { Component } from 'solid-js';
import Header from '../Header/header';
import SearchPoll from '../Poll/Poll-Search/searchPoll';
import CreatePoll from '../Poll/Poll-Creation/createPoll';

const Homepage: Component = () => {
    return (
        <div>
            <Header />
            <SearchPoll />
            {/* Uncomment line below to check create poll setup */}
            {/* <CreatePoll /> */}
        </div>
    )
}

export default Homepage;