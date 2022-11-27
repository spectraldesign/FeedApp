import HomeButton from "./buttons/homeButton";
import Login from "./buttons/loginButton";
import "./header.css"
import ProfileButton from "./buttons/profileButton";

function Header() {
    if ((localStorage.getItem("loggedIn") !== null)) {
        if (localStorage.getItem("loggedIn") == JSON.stringify(true)) {
            return (
                <div class="header-bar">
                    <HomeButton />
                    <ProfileButton />
                </div>
            )
        }
        else {
            return (
                <div class="header-bar">
                <HomeButton />
                <Login />
                </div>
            );
        }
    }
    return (
        <div class="header-bar">
            <HomeButton />
            <Login />
        </div>
    );
  }

  export default Header;

