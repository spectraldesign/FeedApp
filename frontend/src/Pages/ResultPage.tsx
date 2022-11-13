import { Component } from 'solid-js';
import Header from '../components/Header/header';
import ResultPoll from '../components/Poll/Poll-Results/resultPoll';

const ResultPage: Component = () => {
    return (
        <div>
            <Header />
            <ResultPoll />
        </div>
    )
}

export default ResultPage;