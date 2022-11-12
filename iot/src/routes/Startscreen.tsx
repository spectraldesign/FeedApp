import { Component, createEffect, createSignal } from 'solid-js';
import { useNavigate } from '@solidjs/router';
import SearchIoTDevice from '../components/SearchIoTDevice';


const Startscreen: Component= () => {

    return (
        <div class='container-lg' >
            <SearchIoTDevice />
            {/* <button type='button' class='btn btn-primary' onclick={handleClick}> Enter </button> */}
        </div>
    )
}

export default Startscreen;

