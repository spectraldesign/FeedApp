import { Component } from 'solid-js';
import Header from '../components/Header/header';
import ListPolls from '../components/Poll/Poll-List/pollList';
import "../components/button.css";
const Homepage: Component = () => {
    return (
        <div>
            <Header />
            <ListPolls />
        </div>
    )
}

export default Homepage;
