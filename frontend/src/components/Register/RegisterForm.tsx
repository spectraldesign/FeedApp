import { createStore } from "solid-js/store";

type FormFields = {
  firstname?: string;
  lastname?: string;
  username?: string;
  email?: string;
  password?: string;
};

const submit = (form: FormFields) => {
  const dataToSubmit = {
    firstname: form.firstname,
    lastname: form.lastname,
    username: form.username,
    email: form.email,
    password: form.password
  };

  return dataToSubmit;
  // should be submitting your form to some backend service
  console.log(`submitting ${JSON.stringify(dataToSubmit)}`);
};

const registerForm = () => {
  const [form, setForm] = createStore<FormFields>({
    firstname: "",
    lastname: "",
    username: "",
    email: "",
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