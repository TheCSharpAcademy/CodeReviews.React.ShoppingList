interface Prop {
  children: React.ReactNode;
  onClick?: () => void;
  style?: React.CSSProperties;
  type?: "button" | "reset" | "submit" | undefined;
}

export const Button = ({ children, onClick, style, type }: Prop) => {
  return (
    <button
      onClick={onClick}
      style={{
        paddingLeft: "0.5rem",
        paddingRight: "0.5rem",
        ...style,
      }}
      type={type}
    >
      {children}
    </button>
  );
};
