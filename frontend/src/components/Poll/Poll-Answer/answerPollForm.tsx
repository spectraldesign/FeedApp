import { createStore } from "solid-js/store";

type FormFields = {
  isPositive?: boolean;
};

const submit = (form: FormFields) => {
  const dataToSubmit = {
    isPositive: form.isPositive
  };

  return dataToSubmit;
  // should be submitting your form to some backend service
  console.log(`submitting ${JSON.stringify(dataToSubmit)}`);
};

const answerForm = () => {
  const [form, setForm] = createStore<FormFields>({
    isPositive: true
  });

  const clearField = (fieldName: string) => {
    setForm({
      [fieldName]: ""
    });
  };

  const updateFormField = (fieldName: string) => (event: Event) => {
    const inputElement = event.currentTarget as HTMLInputElement;
    if (inputElement.value === "true") {
        setForm({
            [fieldName]: true
          });
    } else {
        setForm({
            [fieldName]: false
          });
    }
  };

  return { form, submit, updateFormField, clearField };
};

export { answerForm };