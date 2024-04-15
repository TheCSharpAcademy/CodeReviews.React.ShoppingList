import { useEffect, useRef, useState } from "react";
import { Button } from "../utils/Button";
import "./modal.css";

interface Props {
  isOpened?: boolean;
  hasCloseBtn?: boolean;
  onClose?: () => void;
  children?: React.ReactNode;
}

export const Modal = ({
  isOpened: isOpen,
  hasCloseBtn,
  onClose,
  children,
}: Props) => {
  const modalRef = useRef<HTMLDialogElement | null>(null);
  const [modalOpen, setModalOpen] = useState(isOpen);

  useEffect(() => {
    setModalOpen(isOpen);
  }, [isOpen]);

  useEffect(() => {
    const modalElement = modalRef.current;

    if (modalElement) {
      if (modalOpen) {
        modalElement.showModal();
      } else {
        modalElement.close();
      }
    }
  }, [modalOpen]);

  const handleModalClose = () => {
    if (onClose) {
      onClose();
    }

    setModalOpen(false);
  };

  const handleKeyDown = (event: React.KeyboardEvent<HTMLDialogElement>) => {
    if (event.key === "Escape") {
      handleModalClose();
    }
  };

  return (
    <dialog onKeyDown={handleKeyDown} ref={modalRef}>
      {hasCloseBtn && (
        <Button
          style={{
            fontSize: "0.75em",
            position: "absolute",
            top: "0.25em",
            right: "0.25em",
            padding: "0.6em 1.2em",
            borderRadius: "8px",
          }}
          onClick={handleModalClose}
        >
          ❌
        </Button>
      )}
      {children}
    </dialog>
  );
};
