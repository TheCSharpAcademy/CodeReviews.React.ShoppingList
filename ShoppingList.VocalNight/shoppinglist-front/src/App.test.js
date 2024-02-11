import { render, screen } from '@testing-library/react';
import App from './App';
import Item from './Components/Item';
import userEvent from '@testing-library/user-event'
import { act } from '@testing-library/react';

test('renders App', () => {
  render(<App />);
  render(<Item key={1}
  data-testid={"Pasta"}
  shoppingItem={{id: 1, isPickedUp: false, name: "Pasta"}}
  fetchData={() => null}/>);
});

test('renders input text', () => {
  render(<App />);

  const inputVal = screen.getByTestId("itemInput");
  expect(inputVal).toBeInTheDocument();
  expect(inputVal).toHaveAttribute("type", "text");
});

test('renders input button', () => {
  render(<App />);

  const inputBut = screen.getByTestId("AddItemBut");
  expect(inputBut).toBeInTheDocument();
});


test('renders input button', () => {
  render(<App />);

  const inputBut = screen.getByTestId("AddItemBut");
  expect(inputBut).toBeInTheDocument();
});

test('renders item', () => {
  render(<App />);
  render(<Item key={1}
  data-testid={"Pasta"}
  shoppingItem={{id: 1, isPickedUp: false, name: "Pasta"}}
  fetchData={() => null}/>);

  const checkbox = screen.getByTestId("checkboxItem");
  expect(checkbox).toBeInTheDocument();

  const deleteBt = screen.getByTestId("itemDelete");
  expect(deleteBt).toBeInTheDocument();
});

test('checkbox works', () => {
  render(<App />);
  const {container} = render(<Item key={1}
  data-testid={"Pasta"}
  shoppingItem={{id: 1, isPickedUp: false, name: "Pasta"}}
  fetchData={() => null}/>);

  const checkbox = screen.getByTestId("checkboxItem");
  expect(checkbox).toBeInTheDocument();

    act(() => {
      userEvent.click(checkbox);
    });

    expect(screen.getByTestId("checkboxLabel")).toHaveClass('checkboxLined');
});
