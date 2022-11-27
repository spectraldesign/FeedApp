import "./Logout.css";
import "../button.css"
import toast from "solid-toast";
import { useNavigate, NavLink } from '@solidjs/router';

function Logout() {
    const navigate = useNavigate();
    const logoutFunc = () => {
        localStorage.clear();
        //alert('Logout successful');
        toast.success("Logout successful", {position:"bottom-center", style: {'background-color': '#cdf2cb',}})
        navigate('/');
      }
    
    return (
        <div>
            <div class="logout">
                <button class="submit-btn" onClick={logoutFunc}>Logout</button>
            </div>
        </div>
    );
  }

  export default Logout;

