import { Component } from 'solid-js';
import Header from '../Header/header';
import SearchPoll from '../Poll/Poll-Search/searchPoll';

const Homepage: Component = () => {
    return (
        <div>
            <Header />
            <SearchPoll />
        </div>
    )
}

export default Homepage;