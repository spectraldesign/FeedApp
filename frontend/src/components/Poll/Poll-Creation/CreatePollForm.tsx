import { createStore } from "solid-js/store";

type FormFields = {
  question?: string;
  endTime?: string;
  isPrivate: boolean;
};

const submit = (form: FormFields) => {
  const dataToSubmit = {
    question: form.question,
    isPrivate: form.isPrivate,
    endTime: form.endTime
  };

  return dataToSubmit;
  // should be submitting your form to some backend service
  console.log(`submitting ${JSON.stringify(dataToSubmit)}`);
};

const createForm = () => {
  const [form, setForm] = createStore<FormFields>({
    question: "",
    endTime: "",
    isPrivate: false
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

export { createForm };