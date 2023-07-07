package kr.pah.pcs.flatformer_be.service;

import kr.pah.pcs.flatformer_be.domain.Ranking;
import kr.pah.pcs.flatformer_be.dto.RankingDto;
import kr.pah.pcs.flatformer_be.repository.RankingRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@RequiredArgsConstructor
@Transactional
public class RankingService {

    private final RankingRepository rankingRepository;

    public String insert(RankingDto rankingDto) {
        Ranking user = rankingRepository.findByPlayerName(rankingDto.getPlayerName());
        if (user != null) {
            if (user.getTime() > rankingDto.getTime())
            user.modified(rankingDto.getPlayerName(), rankingDto.getTime());
        }else {
            Ranking newUser = new Ranking(null, rankingDto.getPlayerName(), rankingDto.getTime());
            rankingRepository.save(newUser);
        }
        return "ok";
    }
}
