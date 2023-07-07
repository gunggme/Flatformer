package kr.pah.pcs.flatformer_be.repository;

import jakarta.persistence.EntityManager;
import kr.pah.pcs.flatformer_be.domain.Ranking;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.jpa.repository.JpaRepository;

public interface RankingRepository extends JpaRepository<Ranking, Long> {
    Ranking findByPlayerName(String playerName);
}
