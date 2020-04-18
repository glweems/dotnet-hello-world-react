import React from "react";
import useAxios from "axios-hooks";
import { TodoItem } from "../../Models/todoItem";
import {
  CircularProgress,
  TextField,
  Container,
  Button,
  Checkbox,
} from "@material-ui/core";
import Todos from "./components/Todos";
import { useFormik } from "formik";
function App() {
  const [{ data, loading, error, response }, todoItemsApi] = useAxios<
    TodoItem[]
  >("/api/todoitems");
  const formik = useFormik({
    initialValues: { name: "" },
    onSubmit: async (values) => {
      await todoItemsApi({
        method: "post",
        url: response?.config.url,
        data: values,
      });
    },
  });
  if (loading) return <CircularProgress />;
  if (error) return <Container>{error.message}</Container>;
  const { handleChange, handleBlur } = formik;
  return (
    <Container className="App">
      <form onSubmit={formik.handleSubmit}>
        <TextField
          label="Name"
          name="name"
          onChange={handleBlur}
          onBlur={handleChange}
        />
        <Checkbox />
        <Button type="submit" variant="contained" color="primary">
          Add Todo
        </Button>
      </form>
      <Todos todos={data} />
    </Container>
  );
}

export default App;
