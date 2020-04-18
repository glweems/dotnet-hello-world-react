import React from "react";
import { TodoItem } from "../../../Models/todoItem";
import { List, ListItem, ListItemText } from "@material-ui/core";
interface Props {
  todos: TodoItem[];
}

const Todos = ({ todos }: Props) => {
  return (
    <List>
      {todos.map((todo) => (
        <ListItem key={todo.id}>
          <ListItemText>{todo.name}</ListItemText>
        </ListItem>
      ))}
    </List>
  );
};

export default Todos;
