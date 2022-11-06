import { createStore } from "solid-js/store";

type FormFields = {
  firstname?: string;
  lastname?: string;
  email?: string;
  username?: string;
  password?: string;
};

const submit = (form: FormFields) => {
  const dataToSubmit = {
    firstname: form.firstname,
    lastname: form.lastname,
    email: form.email,
    userName: form.username,
    password: form.password
  };

  return dataToSubmit;
};

const registerForm = () => {
  const [form, setForm] = createStore<FormFields>({
    firstname: "",
    lastname: "",
    email: "",
    username: "",
    password: ""
  });

  const clearField = (fieldName: string) => {
    setForm({
      [fieldName]: ""
    });
  };

  const updateFormField = (fieldName: string) => (event: Event) => {
    const inputElement = event.currentTarget as HTMLInputElement;
    if (inputElement.type === "checkbox") {
      setForm({
        [fieldName]: !!inputElement.checked
      });
    } else {
      setForm({
        [fieldName]: inputElement.value
      });
    }
  };

  return { form, submit, updateFormField, clearField };
};

export { registerForm };