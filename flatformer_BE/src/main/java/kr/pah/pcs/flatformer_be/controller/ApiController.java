package kr.pah.pcs.flatformer_be.controller;

import kr.pah.pcs.flatformer_be.domain.Ranking;
import kr.pah.pcs.flatformer_be.dto.RankingDto;
import kr.pah.pcs.flatformer_be.repository.RankingRepository;
import kr.pah.pcs.flatformer_be.service.RankingService;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.web.PageableDefault;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequiredArgsConstructor
public class ApiController {

    private final RankingRepository rankingRepository;
    private final RankingService rankingService;

    @GetMapping("/api/ranking")
    public Page<Ranking> ranking(@PageableDefault Pageable pageable) {
        Pageable page = PageRequest.of(pageable.getPageNumber(), pageable.getPageSize(), Sort.by("time").ascending());
        return rankingRepository.findAll(page);
    }

    @PostMapping("/api/insert")
    public String insert(@RequestBody RankingDto rankingDto) {
        return rankingService.insert(rankingDto);
    }
}
