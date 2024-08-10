const Paginator = ({
  currentPage,
  totalPages,
  prevPage,
  nextPage,
}: {
  currentPage: number;
  totalPages: number | null | undefined;
  prevPage: () => void;
  nextPage: () => void;
}) => {
  return totalPages ? (
    <div className="flex items-center gap-4 justify-end w-full">
      <button onClick={prevPage}>{"<"}</button>
      <div>
        {currentPage} / {totalPages}
      </div>
      <button onClick={nextPage}>{">"}</button>
    </div>
  ) : null;
};

export default Paginator;
