import { Component } from 'solid-js';
<<<<<<< HEAD:frontend/src/Pages/Homepage.tsx
import Header from '../components/Header/header';
import SearchPoll from '../components/Poll/Poll-Search/searchPoll';
=======
import Header from '../Header/header';
import SearchPoll from '../Poll/Poll-Search/searchPoll';
import CreatePoll from '../Poll/Poll-Creation/createPoll';
>>>>>>> main:frontend/src/components/Pages/Homepage.tsx

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
