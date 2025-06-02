using MonopolyStorage.Domain.Models;

namespace MonopolyStorage.Presentation.CommandsCache
{
    public class CommandsCacheStorage()
    {
        private List<Pallet> _pallets { get; } = [];
        private List<Box> _boxes { get; } = [];

        public IReadOnlyCollection<Pallet> GetAllPallets() => _pallets.AsReadOnly();
        public IReadOnlyCollection<Box> GetAllBoxes() => _boxes.AsReadOnly();

        public void AddPallet(Pallet pallet)
        {
            if (_pallets.Any(p => p.Id == pallet.Id))
                throw new ArgumentException($"Паллета с Id {pallet.Id} уже присутствует в наборе данных.");
            _pallets.Add(pallet);
        }
        public void AddBox(Box box)
        {
            if (_boxes.Any(b => b.Id == box.Id))
                throw new ArgumentException($"Коробка с Id {box.Id} уже присутствует в наборе данных.");
            _boxes.Add(box);
        }

        public void AddPallets(IEnumerable<Pallet> pallets)
        {
            var duplicates = _pallets.Join(pallets, p1 => p1.Id, p2 => p2.Id, (p1, p2) => new { p1.Id });

            if (duplicates.Any())
            {
                var duplicatesString = duplicates.Select(a => a.Id.ToString());
                throw new ArgumentException($"Паллеты с Id '{string.Join(',', duplicatesString)}' уже присутствует в наборе данных.");
            }
            _pallets.AddRange(pallets);
        }

        public void AddBoxes(IEnumerable<Box> boxes)
        {
            var duplicates = _boxes.Join(boxes, p1 => p1.Id, p2 => p2.Id, (p1, p2) => new { p1.Id });

            if (duplicates.Any())
            {
                var duplicatesString = duplicates.Select(a => a.Id.ToString());
                throw new ArgumentException($"Коробки с Id '{string.Join(',', duplicatesString)}' уже присутствует в наборе данных.");
            }
            _boxes.AddRange(boxes);
        }

        public void MergeBox(Guid palletId, Box box)
        {
            var pallet = _pallets.FirstOrDefault(p => p.Id == palletId);
            pallet?.AddBoxOnPallet(box);
        }
    }
}
