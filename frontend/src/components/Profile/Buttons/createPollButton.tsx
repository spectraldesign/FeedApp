import { NavLink } from "@solidjs/router";
import "../../button.css"

function CreatePollButton() {
    return (
        <>
            <div class="button createpoll">
                <NavLink href="/poll/create">Create Poll</NavLink>
            </div>
        </>
    );
  }

  export default CreatePollButton;

