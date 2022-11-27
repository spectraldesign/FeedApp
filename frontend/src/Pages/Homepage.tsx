import { Component } from 'solid-js';
import Header from '../components/Header/header';
import PollList from '../components/Poll/Poll-List/pollList';
import SearchPoll from '../components/Poll/Poll-Search/searchPoll';
import APIButton from '../components/API/apiButton';
import "../components/button.css";
const Homepage: Component = () => {
    return (
        <div>
            <Header />
            <SearchPoll />
            <PollList />
            <APIButton />
        </div>
    )
}

export default Homepage;
