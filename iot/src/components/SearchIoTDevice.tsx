import { id, setId, iot_device, setIot_device } from '../routes/Votescreen';
import "./SearchIoTDevice.css";
import { useNavigate } from '@solidjs/router';
import axios from 'axios';
import { Component, createEffect, createResource, createSignal } from 'solid-js';




function SearchIoTDevice (){
    
    const navigate = useNavigate();
    const handleChange = (e: any) => {
        setId(e.target.value);
        console.log("hello you", id());
    }

    const handleSubmit = async () => {
            const res = await axios.get(`https://localhost:7280/api/IoT/${id()}`);
            const data = await res.data;
            setIot_device(data);
            navigate('/id');
    }

    return (
        <div class="container">
            <div class="iot-search">
            <h1 class='text-center'> IoT - Find a device </h1>
                <div class="iot-search-form">
                    <input type="text" class="iot-search-input" placeholder="&#128269; Search for IoT Device" onChange={handleChange} />
                    <button class="submit-iot-btn" type="submit" onClick={handleSubmit} > Enter </button>
                </div>
            </div> 
        </div>
    );
}
export default SearchIoTDevice;
