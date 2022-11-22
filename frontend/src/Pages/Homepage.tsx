import { Component } from 'solid-js';
import Header from '../components/Header/header';
import CreatePoll from '../components/Poll/Poll-Creation/createPoll';
import SearchPoll from '../components/Poll/Poll-Search/searchPoll';
import APIButton from '../components/API/apiButton';
import "../components/button.css";
const Homepage: Component = () => {
    return (
        <div>
            <Header />
            <SearchPoll />
            <APIButton />
        </div>
    )
}

export default Homepage;
