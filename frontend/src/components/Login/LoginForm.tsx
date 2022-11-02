import { createStore } from "solid-js/store";

type FormFields = {
  userName?: string;
  password?: string;
};

const submit = (form: FormFields) => {
  const dataToSubmit = {
    userName: form.userName,
    password: form.password
  };

  return dataToSubmit;
  // should be submitting your form to some backend service
  console.log(`submitting ${JSON.stringify(dataToSubmit)}`);
};

const loginForm = () => {
  const [form, setForm] = createStore<FormFields>({
    userName: "",
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

export { loginForm };