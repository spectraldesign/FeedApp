import { id, setId, iot_device, setIot_device, setPoll, poll, setOnePoll, onePoll, setPoll_id, poll_id} from '../routes/Votescreen';
import "./SearchIoTDevice.css";
import { useNavigate } from '@solidjs/router';
import axios from 'axios';
import getPolls from '../components/Poll';
import { Component, createEffect, createResource, createSignal } from 'solid-js';


function SearchIoTDevice (){
    const navigate = useNavigate();
    const handleChange = (e: any) => {
        setId(e.target.value);
    }

    const handleSubmit = async () => {
            setPoll('');
            setOnePoll('');
            const res = await axios.get(`${import.meta.env.VITE_BASE_URL}IoT/${id()}`).catch(err => err);
            if(!res || !res.status){ return 400 }
            const data = await res.data;
            setIot_device(data);
            return 200
    }

    const getPolls = async () => {
        const res = await axios.get(`${import.meta.env.VITE_BASE_URL}IoT/servedPolls/${id()}`).catch(err => err);
        if(!res || !res.status){return 400}
        const data = await res.data;
        if(data.length == 0){
            return 200
        }
        setPoll(data);
        setOnePoll(poll()[0]);
        setPoll_id(onePoll()["id"]);
        return 200
    }

    async function onClick() {
        const res = await handleSubmit();
        if(res != 200){
            alert('IoT device not found.')
            return navigate('');
        }
        const res2 = await getPolls();
        if (res2 != 200){
            alert('An error ocurred while fetching polls. Please try again.')
            return navigate('')
        }
        if (res == 200 && res2 == 200){
            return navigate('/id');
        }
    }

    return (
        <div class="container">
            <div class="iot-search">
            <h1 class='text-center'> IoT - Find a device </h1>
                <div class="iot-search-form">
                    <input type="text" class="iot-search-input" placeholder="&#128269; Search for IoT Device" onChange={handleChange} />
                    <button class="submit-iot-btn" type="submit" onClick={onClick} > Enter </button>
                </div>
            </div> 
        </div>
    );
}
export default SearchIoTDevice;
