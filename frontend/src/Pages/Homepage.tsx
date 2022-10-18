import { Component } from 'solid-js';
import Header from '../components/Header/header';
import SearchPoll from '../components/Poll/Poll-Search/searchPoll';

const Homepage: Component = () => {
    return (
        <div>
            <Header />
            <SearchPoll />
        </div>
    )
}

export default Homepage;